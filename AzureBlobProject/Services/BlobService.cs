
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
                _blobServiceClient = blobServiceClient;
        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            //retrieve all teh blobs using blobCOntclient
            var blobs = blobContainerClient.GetBlobsAsync();

            //once we have all the blobs, we can iterate through loop and we can 
            //add the file to a object
            var blobString = new List<string>();
            await foreach (var blob in blobs)
            {
                blobString.Add(blob.Name);
            }

            return blobString;
        }

        public async Task<string> GetBlobs(string name, string containerName)
        {
            //to retrieve a single blob we can do that using url
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);
            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);

             return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            //built in method to upload
            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            if(result != null)
            {
                return true;
            }
            return false;
        }
    }
}
