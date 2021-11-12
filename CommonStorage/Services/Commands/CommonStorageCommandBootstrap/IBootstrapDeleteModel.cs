using OpAdminDomain;
using OpAdminStorageLibrary;
using OpAdminStorageLibrary.Commands.CommonCommand;

namespace CommonStorageCommandBootstrap
{
    public interface IBootstrapDeleteModel
    {
        ICommandDeleteModel BootstrapDeleteModelCommand(string connectionString);
    }
    
    public class BootstrapDeleteModel : IBootstrapDeleteModel
    {
        public ICommandDeleteModel BootstrapDeleteModelCommand(string connectionString) =>
            new DeleteModelCommand(new CommandConnection(connectionString));
    }
}