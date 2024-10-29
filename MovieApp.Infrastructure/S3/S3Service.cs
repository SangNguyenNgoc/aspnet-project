using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MovieApp.Infrastructure.S3;

public class S3Service
{
    private readonly string? _endpointUrl = Environment.GetEnvironmentVariable("ENDPOINT");
    private readonly string? _bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME");
    private readonly IAmazonS3 _s3Client;
    private readonly ILogger<S3Service> _logger;

    public S3Service(IAmazonS3 s3Client, ILogger<S3Service> logger)
    {
        _s3Client = s3Client;
        _logger = logger;
    }

    private async Task<FileInfo> ConvertMultipartToFile(IFormFile file)
    {
        var tempFilePath = Path.GetTempFileName();
        await using var stream = new FileStream(tempFilePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return new FileInfo(tempFilePath);
    }

    private string GetFileExtension(IFormFile file)
    {
        return Path.GetExtension(file.FileName)?.TrimStart('.') ?? string.Empty;
    }

    private string GenerateFileName(string slug, string folder, string extension)
    {
        return $"{folder}/{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}-{slug}.{extension}";
    }

    private async Task UploadFileToS3Bucket(string fileName, FileInfo file)
    {
        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                FilePath = file.FullName,
                CannedACL = S3CannedACL.PublicRead
            };

            var response = await _s3Client.PutObjectAsync(putRequest);
            _logger.LogInformation("File uploaded successfully. ETag: {ETag}", response.ETag);
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, "Error uploading file to S3");
            throw new Exception("Failed to upload file to S3", ex);
        }
    }

    public async Task<string> UploadFile(IFormFile multipartFile, string slug, string folder)
    {
        try
        {
            var file = await ConvertMultipartToFile(multipartFile);
            var extension = GetFileExtension(multipartFile);
            var fileName = GenerateFileName(slug, folder, extension);
            var fileUrl = $"{_endpointUrl}/{_bucketName}/{fileName}";

            await UploadFileToS3Bucket(fileName, file);
            file.Delete();

            return fileUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Uploading image failed");
            throw new Exception("Server error while uploading image", ex);
        }
    }
}