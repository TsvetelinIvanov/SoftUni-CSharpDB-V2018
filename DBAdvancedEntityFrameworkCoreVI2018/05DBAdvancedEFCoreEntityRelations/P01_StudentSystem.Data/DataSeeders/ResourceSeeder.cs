using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.DataSeeders
{
    public class ResourceSeeder
    {
        public static Resource[] SeedResources(StudentSystemContext context)
        {
            Resource[] resourcesForSeeding = new Resource[]
            {
                new Resource() {ResourceId = 1, Name = "Introduction", Url = "Url",
                    ResourceType = ResourceType.Video, CourseId = 1},
                new Resource() {ResourceId = 2, Name = "Introduction", Url = "Url",
                    ResourceType = ResourceType.Video, CourseId = 2},
                new Resource() {ResourceId = 3, Name = "Introduction", Url = "Url",
                    ResourceType = ResourceType.Video, CourseId = 3},
                new Resource() {ResourceId = 4, Name = "Introduction", Url = "Url",
                    ResourceType = ResourceType.Video, CourseId = 4},
            };

            return resourcesForSeeding;
        }
    }
}