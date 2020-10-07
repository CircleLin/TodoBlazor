using NUnit.Framework;
using TodoBlazor.Pages;
using Bunit;
using Bunit.TestDoubles.JSInterop;
using System.Linq;

namespace TodoBlazorTests
{
    public class Tests
    {
        [Test]
        public void 顯示待辦清單且有2筆待辦事項()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th >
                                        <th> 刪除 </th >
                                      </tr >
                                      <tr>
                                        <td>Buy Milk</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text'>
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";

            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void 輸入框有值_點Add按鈕_待辦事項出現在待辦清單()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();
            cut.Find("input").Change("New Item");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th >
                                        <th> 刪除 </th >
                                      </tr >
                                      <tr>
                                        <td>Buy Milk</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>New Item</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text' value=''>
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";
            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void 輸入框沒有值_點Add按鈕_待辦清單沒有新增待辦事項()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            cut.Find("input").Change("");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th >
                                        <th> 刪除 </th >
                                      </tr >
                                      <tr>
                                        <td>Buy Milk</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text' value='' >
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";

            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void 輸入框有值_點Add按鈕_輸入框清空()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            cut.Find("input").Change("New Item");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th >
                                        <th> 刪除 </th >
                                      </tr >
                                      <tr>
                                        <td>Buy Milk</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>New Item</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text' value=''>
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";
            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void 點擊刪除鈕並確定_可刪除待辦事項()
        {
            var ctx = new Bunit.TestContext();

            var mockJS = ctx.Services.AddMockJSRuntime();
            mockJS.Setup<bool>("SweetConfirm", "Delete", $"確定要刪除Buy Milk?").SetResult(true);

            var cut = ctx.RenderComponent<Index>();

            var firstDeleteButton = cut.FindAll("button").FirstOrDefault();

            firstDeleteButton.Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th>
                                        <th>刪除</th>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text'>
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";

            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void 點擊刪除鈕但取消_不會刪除待辦事項()
        {
            var ctx = new Bunit.TestContext();

            var mockJS = ctx.Services.AddMockJSRuntime();
            mockJS.Setup<bool>("SweetConfirm", "Delete", $"確定要刪除Buy Milk?").SetResult(false);

            var cut = ctx.RenderComponent<Index>();

            var firstDeleteButton = cut.FindAll("button").FirstOrDefault();

            firstDeleteButton.Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>待辦事項</th>
                                        <th> 刪除 </th>
                                      </tr>
                                      <tr>
                                        <td>Buy Milk</td>
                                           <td>
                                             <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>Buy Apple</td>
                                        <td>
                                          <button class='btn btn-danger btn-sm' >delete</button>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </div>
                                <div>
                                  <label>New Todo:</label>
                                  <input type='text'>
                                  <button class='btn btn-primary' id='AddBtn'>Add</button>
                                </div>
                                <br>";

            cut.MarkupMatches(expectedHtml);
        }
    }
}