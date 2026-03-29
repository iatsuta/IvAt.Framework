using ExampleApp.Domain;

using FluentNHibernate.Mapping;

namespace ExampleApp.Infrastructure.Mapping;

public class EmployeeMap : ClassMap<Employee>
{
    public EmployeeMap()
    {
        this.Schema("app");

        this.DynamicUpdate();

        this.Id(x => x.Id).GeneratedBy.GuidComb();

        this.Map(x => x.Login).Not.Nullable();
    }
}