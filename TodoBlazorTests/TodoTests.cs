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
        public void ��ܫݿ�M��B��2���ݿ�ƶ�()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th >
                                        <th> �R�� </th >
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
        public void ��J�ئ���_�IAdd���s_�ݿ�ƶ��X�{�b�ݿ�M��()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();
            cut.Find("input").Change("New Item");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th >
                                        <th> �R�� </th >
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
        public void ��J�بS����_�IAdd���s_�ݿ�M��S���s�W�ݿ�ƶ�()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            cut.Find("input").Change("");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th >
                                        <th> �R�� </th >
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
        public void ��J�ئ���_�IAdd���s_��J�زM��()
        {
            var ctx = new Bunit.TestContext();

            var cut = ctx.RenderComponent<Index>();

            cut.Find("input").Change("New Item");
            cut.Find("#AddBtn").Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th >
                                        <th> �R�� </th >
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
        public void �I���R���s�ýT�w_�i�R���ݿ�ƶ�()
        {
            var ctx = new Bunit.TestContext();

            var mockJS = ctx.Services.AddMockJSRuntime();
            mockJS.Setup<bool>("SweetConfirm", "Delete", $"�T�w�n�R��Buy Milk?").SetResult(true);

            var cut = ctx.RenderComponent<Index>();

            var firstDeleteButton = cut.FindAll("button").FirstOrDefault();

            firstDeleteButton.Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th>
                                        <th>�R��</th>
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
        public void �I���R���s������_���|�R���ݿ�ƶ�()
        {
            var ctx = new Bunit.TestContext();

            var mockJS = ctx.Services.AddMockJSRuntime();
            mockJS.Setup<bool>("SweetConfirm", "Delete", $"�T�w�n�R��Buy Milk?").SetResult(false);

            var cut = ctx.RenderComponent<Index>();

            var firstDeleteButton = cut.FindAll("button").FirstOrDefault();

            firstDeleteButton.Click();

            string expectedHtml = @"<div>
                                  <table class='table table-hover'>
                                    <tbody>
                                      <tr>
                                        <th>�ݿ�ƶ�</th>
                                        <th> �R�� </th>
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