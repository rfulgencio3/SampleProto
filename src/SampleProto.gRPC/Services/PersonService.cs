using SampleProto.Core.Models;
using SampleProto.Infrastructure.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace SampleProto.gRPC.Services;

public class PersonService : SampleProto.gRPC.PersonService.PersonServiceBase
{
    private readonly PersonDbContext _context;

    public PersonService(PersonDbContext context)
    {
        _context = context;
    }

    public override async Task<AddPersonReply> AddPerson(AddPersonRequest request, ServerCallContext context)
    {
        var person = new Person(
            name: request.Name,
            surname: request.Surname,
            address: request.Address,
            zipCode: request.Zipcode,
            state: request.State,
            country: request.Country,
            email: request.Email,
            birthDate: DateTime.Parse(request.BirthDate)
        );

        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        return new AddPersonReply { Message = "Person added successfully" };
    }

    public override async Task<GetPersonByIdReply> GetPersonById(GetPersonByIdRequest request, ServerCallContext context)
    {
        var personId = Guid.Parse(request.Id);
        var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personId);

        if (person == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
        }

        var reply = new GetPersonByIdReply
        {
            Person = new PersonMessage
            {
                Id = person.Id.ToString(),
                Name = person.Name,
                Surname = person.Surname,
                Address = person.Address,
                Zipcode = person.ZipCode,
                State = person.State,
                Country = person.Country,
                Email = person.Email,
                BirthDate = person.BirthDate.ToString("yyyy-MM-dd")
            }
        };

        return reply;
    }
}
