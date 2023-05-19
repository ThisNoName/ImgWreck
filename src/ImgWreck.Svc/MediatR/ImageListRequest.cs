using Amazon.S3;
using ImgWreck.Common;
using MediatR;
using System.Text.Json;

namespace ImgWreck.Svc;

public class ImageListRequest : ApiRequest<List<ImageLabel>>
{
    public string BucketName { get; set; } = "who-took-mybucket";
    public string Prefix { get; set; } = string.Empty;
}

public class ImageListHandler : IRequestHandler<ImageListRequest, List<ImageLabel>>
{
    private readonly IAmazonS3 _s3Client;
    public ImageListHandler(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<List<ImageLabel>> Handle(ImageListRequest request, CancellationToken cancellationToken)
    {
        var response = await _s3Client
            .ListObjectsAsync(request.BucketName, request.Prefix, cancellationToken);

        var objlist = response.S3Objects
            .Where(x => !x.Key.EndsWith("/") && !x.Key.EndsWith("-meta"))
            .Select(x => new ImageLabel(x.Key))
            .ToList();

        // load an object from S3, add the labels, and save it back
        var metaresponse = await _s3Client.GetObjectAsync(
            request.BucketName, request.Prefix + "-meta")
            ?? throw new Exception("ImageShed-meta does not exist");

        var labels = JsonSerializer.Deserialize<List<ImageLabel>>(
            new StreamReader(metaresponse.ResponseStream).ReadToEnd())
            ?? new List<ImageLabel>();

        // update objlist from lables, match by key, update lables properties
        foreach (var obj in objlist)
        {
            var label = labels.FirstOrDefault(x => x.Key == obj.Key);
            if (label != null)
            {
                obj.Labels = label.Labels;
            }
        }

        return objlist.ToList(); ;
    }
}

