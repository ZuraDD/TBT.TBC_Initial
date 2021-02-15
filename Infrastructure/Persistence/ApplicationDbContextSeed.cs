using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using GenderTypeEnum = Domain.Enums.GenderTypeEnum;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IApplicationDbContext context)
        {
            if (!context.GenderType.Any())
            {
                var data = Enum.GetValues(typeof(GenderTypeEnum)).Cast<GenderTypeEnum>().ToList()
                    .Select(x => new GenderType {Id = x, Name = x.ToString()});

                await context.GenderType.AddRangeAsync(data);
            }

            if (!context.PhoneNumberType.Any())
            {
                var data = Enum.GetValues(typeof(PhoneNumberTypeEnum)).Cast<PhoneNumberTypeEnum>().ToList()
                    .Select(x => new PhoneNumberType {Id = x, Name = x.ToString()});

                await context.PhoneNumberType.AddRangeAsync(data);
            }

            if (!context.RelationType.Any())
            {
                var data = Enum.GetValues(typeof(RelationTypeEnum)).Cast<RelationTypeEnum>().ToList()
                    .Select(x => new RelationType {Id = x, Name = x.ToString()});

                await context.RelationType.AddRangeAsync(data);
            }

            var cities = new List<string>
            {
                "თბილისი",
                "ბათუმი",
                "ქუთაისი",
                "რუსთავი",
                "გორი",
                "ზუგდიდი",
                "ფოთი",
                "ხაშური",
                "სამტრედია",
                "სენაკი",
                "ზესტაფონი",
                "მარნეული",
                "თელავი",
                "ახალციხე",
                "ქობულეთი",
                "ოზურგეთი",
                "კასპი",
                "ჭიათურა",
                "წყალტუბო",
                "საგარეჯო",
                "გარდაბანი",
                "ბორჯომი",
                "ტყიბული",
                "ხონი",
                "ბოლნისი",
                "ახალქალაქი",
                "გურჯაანი",
                "მცხეთა",
                "ყვარელი",
                "ახმეტა",
                "ქარელი",
                "ლანჩხუთი",
                "დუშეთი",
                "საჩხერე",
                "დედოფლის",
                "ლაგოდეხი",
                "ნინოწმინდა",
                "აბაშა",
                "წნორი",
                "თერჯოლა",
                "მარტვილი",
                "ხობი",
                "წალენჯიხა",
                "ვანი",
                "ბაღდათი",
                "ვალე",
                "ჩხოროწყუ",
                "თეთრიწყარ",
                "დმანისი",
                "ონი",
                "წალკა",
                "ამბროლაურთი",
                "სიღნაღი",
                "ცაგერი",
                "ჯვარი",
                "სოხუმი",
                "ცხინვალი",
                "გაგრა",
                "ოჩამჩირე",
                "გუდაუთა",
                "გალი",
                "ტყვარჩელი",
                "ახალი"
            };

            if (!context.City.Any())
            {
                await context.City.AddRangeAsync(cities.Select(x => new City { Name = x.ToString() }));
            }

            await context.SaveChangesAsync();
        }
    }
}
