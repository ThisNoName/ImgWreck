using Amazon.S3;
using Amazon.S3.Model;
using MediatR;
using System.Text.Json;

namespace ImgWreck.Svc;

public class DocMetaRequest : ApiRequest<List<DocMeta>>
{
    public string BucketName { get; set; } = null!;
    public string Prefix { get; set; } = null!;
}

public class DocMetaHandler : IRequestHandler<DocMetaRequest, List<DocMeta>>
{
    private readonly IAmazonS3 _s3Client;
    public DocMetaHandler(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<List<DocMeta>> Handle(DocMetaRequest request, CancellationToken cancellationToken)
    {
        var doclist = (await _s3Client.ListObjectsAsync(
            request.BucketName, request.Prefix, cancellationToken))
            .S3Objects.Where(x => !x.Key.EndsWith("/"))
            .ToList();

        //var metalist = (await _s3Client.ListObjectsAsync(
        //    request.BucketName, "DocMeta/", cancellationToken))
        //    .S3Objects.Where(x => !x.Key.EndsWith("/"))
        //    .ToList();

        List<DocMeta> result = new List<DocMeta>();

        foreach (var doc in doclist)
        {
            GetObjectResponse metaresponse;

            try
            {
                metaresponse = await _s3Client.GetObjectAsync(
                   request.BucketName, doc.Key.Replace("DocShed/", "DocMeta/") + ".txt");

                if (metaresponse == null) continue;

                // deserialize the metaresponse
                var meta = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    new StreamReader(metaresponse.ResponseStream).ReadToEnd())
                    ?? new Dictionary<string, string>();

                result.Add(new DocMeta { DocKey = doc.Key, DocMetas = meta });

            }
            catch (Exception ex)
            {
                continue;
            }



            if (metaresponse == null) continue;


        }

        return result;
    }
}

