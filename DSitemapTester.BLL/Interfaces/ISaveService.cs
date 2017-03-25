using DSitemapTester.Entities.Entities;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ISaveService
    {
        bool SaveTestData(WebResourceTest webResourceTest, Tester.Entities.TesterTest test);

        WebResourceTest GetNewTest(string url);
    }
}
