using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.Textract;
using Amazon.Textract.Model;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ImgWreck.Doc;

public class Function
{
    IAmazonS3 _s3client { get; set; }

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
        _s3client = new AmazonS3Client(RegionEndpoint.USEast1);
    }

    /// <summary>
    /// Extract text from document uploaded to S3
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        // using Textract API to dectect document stored on S3
        foreach (var record in evnt.Records)
        {
            var s3Event = record.S3;
            if (s3Event == null) continue;

            var client = new AmazonTextractClient(RegionEndpoint.USEast1);

            // analyze s3 object as ID document
            var response = await client.AnalyzeIDAsync(new AnalyzeIDRequest
            {
                DocumentPages = new List<Document>
                {
                    new Document {
                        S3Object = new S3Object
                        {
                            Bucket = s3Event.Bucket.Name,
                            Name = s3Event.Object.Key
                        }
                    }
                }
            });

            foreach (var doc in response.IdentityDocuments)
            {
                var result = doc.IdentityDocumentFields
                      .Select(s => new KeyValuePair<string, string>(s.Type.Text, s.ValueDetection.Text))
                      .ToDictionary(k => k.Key, v => v.Value);

                // !!! DO NOT USE s3Event.Object.Key (DocShed/)   Bucket will blow up !!!
                var metakey = s3Event.Object.Key.Replace("DocShed/", "DocMeta/") + ".txt";

                var putRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    // !!! DO NOT USE s3Event.Object.Key (DocShed/)   Bucket will blow up !!!
                    Key = metakey,
                    // !!! DO NOT USE s3Event.Object.Key !!!
                    ContentBody = JsonSerializer.Serialize(result)
                };

                await _s3client.PutObjectAsync(putRequest);
                Console.WriteLine(putRequest.Key + " updated");
            }
        }
        // using S3 API to detect document stored on S3
        //foreach (var record in evnt.Records)
        //{
        //    var s3Event = record.S3;
        //    if (s3Event == null) continue;
        //    var request = new GetObjectRequest
        //    {
        //        BucketName = s3Event.Bucket.Name,
        //        Key = s3Event.Object.Key
        //    };
        //    var response = await S3Client.GetObjectAsync(request);
        //    using var responseStream = response.ResponseStream;
        //    using var reader = new StreamReader(responseStream);
        //    var image = reader.ReadToEnd();
        //    var text = await ExtractText(image);
        //    var putRequest = new PutObjectRequest
        //    {
        //        BucketName = s3Event.Bucket.Name,
        //        Key = $"{s3Event.Object.Key}.txt",
        //

        // copilot init recommendataion
        //foreach (var record in evnt.Records)
        //{
        //    var s3Event = record.S3;
        //    if (s3Event == null) continue;
        //    var request = new GetObjectRequest
        //    {
        //        BucketName = s3Event.Bucket.Name,
        //        Key = s3Event.Object.Key
        //    };
        //    var response = await S3Client.GetObjectAsync(request);
        //    using var responseStream = response.ResponseStream;
        //    using var reader = new StreamReader(responseStream);
        //    var image = reader.ReadToEnd();
        //    var text = await ExtractText(image);
        //    var putRequest = new PutObjectRequest
        //    {
        //        BucketName = s3Event.Bucket.Name,
        //        Key = $"{s3Event.Object.Key}.txt",
        //        ContentBody = text
        //    };
        //    await S3Client.PutObjectAsync(putRequest);
        //}
    }
}