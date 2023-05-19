using ImgWreck.Common;
using ImgWreck.Svc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImgWreck.Api.Controllers;

public class ImageController : BaseController
{
    private readonly ISender _sender;
    public ImageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/Image/List")]
    public async Task<List<ImageLabel>> ListAsync()
    {
        return await _sender.Send(new ImageListRequest
        {
            BucketName = "who-took-mybucket",
            Prefix = "ImageShed"
        });
    }

    // copilot auto generated
    //[HttpGet("/Image/List")]
    //public async Task<string> ListAsync() {
    //    return await _sender.Send(new ImageListRequest
    //    {
    //        BucketName = "imgwreck",
    //        Prefix = "images"
    //    });
    //}

    // Get tags list from ImageShed
    [HttpGet("/Image/Tags")]
    public async Task<Dictionary<string, int>> TagsAsync()
    {
        var labels = await _sender.Send(new ImageListRequest
        {
            BucketName = "who-took-mybucket",
            Prefix = "ImageShed"
        });

        // get all tags with count
        var tags = labels
            .SelectMany(l => l.Labels)
            .GroupBy(l => l.Key)
            .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
            .ToDictionary(k => k.Key, v => v.Value);

        return tags;
    }

    // Get ImgDoc list from DocShed, merge with info from DocMeta
    [HttpGet("Image/Docs")]
    public async Task<List<DocMeta>> DocsAsync()
    {
        var docs = await _sender.Send(new DocMetaRequest
        {
            BucketName = "who-took-mybucket",
            Prefix = "DocShed/"
        });

        return docs;
    }
}
