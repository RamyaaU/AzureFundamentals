
using Azure.Storage.Blobs;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainerService
    {
        //as this was injected in startup, like dbcontext it can be called
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobClient)
        {
            _blobServiceClient = blobClient;
        }
        public Task CreateContainer(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContainer(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainer()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
