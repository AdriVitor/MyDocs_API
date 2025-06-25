using MyDocs.Features.Users.Create;
using MyDocs.Models;

namespace MyDocs.Tests.Shared
{
    public static class GenerateModelsService
    {
        public static Document CreateDocument(int id, int idUser, string fileName, string fileType, long fileSize, string uniqueFileName)
        {
            return new Document
            {
                Id = id,
                IdUser = idUser,
                FileName = fileName,
                FileType = fileType,
                FileSize = fileSize,
                UniqueFileName = uniqueFileName
            };
        }

        public static User CreateUser(int id, string name, string cpf, DateTime dateOfBirth, string phone)
        {
            return new User
            {
                Id = id,
                Name = name,
                CPF = cpf,
                DateOfBirth = dateOfBirth,
                Phone = phone
            };
        }

        public static Alert CreateAlert(int id, int idUser,string name, string description, int recurrenceOfSending, DateTime CreationDate, DateTime? endDate = null)
        {
            return new Alert
            {
                Id = id,
                IdUser = idUser,
                Name = name,
                Description = description,
                RecurrenceOfSending = recurrenceOfSending,
                CreationDate = CreationDate,
                EndDate = endDate
            };
        }
    }
}
