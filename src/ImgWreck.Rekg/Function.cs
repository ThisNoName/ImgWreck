using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using ImgWreck.Common;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ImgWreck.Rekg;

public class Function
{
    private IAmazonS3 _s3client { get; set; }
    private readonly IAmazonRekognition _rekgclient;

    public Function()
    {
        _s3client = new AmazonS3Client(region: Amazon.RegionEndpoint.USEast1);
        _rekgclient = new AmazonRekognitionClient();
    }


    /// <summary>
    /// Detect lables in an image uploaded to S3
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var eventRecords = evnt.Records ?? new List<S3Event.S3EventNotificationRecord>();
        foreach (var record in eventRecords)
        {
            var s3Event = record.S3;
            if (s3Event == null) continue;

            var request = new DetectLabelsRequest
            {
                Image = new Image
                {
                    S3Object = new Amazon.Rekognition.Model.S3Object
                    {
                        Bucket = s3Event.Bucket.Name,
                        Name = s3Event.Object.Key
                    }
                },
                MaxLabels = 10,
                MinConfidence = 75F
            };

            var image = new ImageLabel(s3Event.Object.Key)
            {
                Labels = new Dictionary<string, float>()
            };

            var response = await _rekgclient.DetectLabelsAsync(request);

            Console.WriteLine("Detected labels for " + s3Event.Object.Key);
            foreach (var label in response.Labels)
            {
                image.Labels.Add(label.Name, label.Confidence);
            }

            // load an object from S3, add the labels, and save it back
            var metaresponse = await _s3client.GetObjectAsync(
                s3Event.Bucket.Name, "ImageShed-meta")
                ?? throw new Exception("ImageShed-meta does not exist");

            var labels = JsonSerializer.Deserialize<List<ImageLabel>>(
                new StreamReader(metaresponse.ResponseStream).ReadToEnd())
                ?? new List<ImageLabel>();

            // update objlist from lables, match by key, update lables properties
            var existing = labels.FirstOrDefault(x => x.Key == image.Key);
            if (existing != null)
            {
                existing.Labels = image.Labels;
            }
            else
            {
                labels.Add(image);
            }

            var putRequest = new PutObjectRequest
            {
                BucketName = s3Event.Bucket.Name,
                // !!! DO NOT USE s3Event.Object.Key (ImgShed/)   Bucket will blow up !!!
                Key = "ImageShed-meta",
                // !!! DO NOT USE s3Event.Object.Key !!!
                ContentBody = JsonSerializer.Serialize(labels)
            };
            await _s3client.PutObjectAsync(putRequest);

            // copilot generated
            //var metaresponse = await _s3client.GetObjectAsync(
            //                s3Event.Bucket.Name, s3Event.Object.Key + "-meta")
            //                ?? throw new Exception("ImgShed.meta does not exist");
            //            var labels = JsonSerializer.Deserialize<List<ImageLabel>>(
            //                new StreamReader(metaresponse.ResponseStream).ReadToEnd())
            //                ?? new List<ImageLabel>();
            //            // update objlist from lables, match by key, update lables properties
            //            var existing = labels.FirstOrDefault(x => x.Key == image.Key);
            //            if (existing != null)
            //            {
            //                existing.Labels = image.Labels;
            //            }
            //            else
            //            {
            //                labels.Add(image);
            //            }
            //            var json = JsonSerializer.Serialize(labels);
            //            var putRequest = new PutObjectRequest
            //            {
            //                BucketName = s3Event.Bucket.Name,
            //                Key = s3Event.Object.Key + "-meta",
            //                ContentBody = json
            //            };
            //            await _s3client.PutObjectAsync(putRequest);
        }
    }
}