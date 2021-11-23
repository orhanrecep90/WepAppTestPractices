using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WepAppTestPractices.MVC.Controllers;
using WepAppTestPractices.MVC.Models;
using WepAppTestPractices.MVC.Repository;
using Xunit;

namespace WepAppTestPractices.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsController _controller;
        private List<Product> products;
        public ProductControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsController(_mockRepo.Object);
            products = new List<Product>() {
                new Product { Color="red",Id=1,Name="pencil",Price=11,Stock=100},
            new Product{Color="Blue",Id=2,Name="rubber",Price=22,Stock=200},
            new Product{Color="Green",Id=3,Name="book",Price=44,Stock=400}
            };
        }
        [Fact]
        public async void Index_ActionExcecutes_ReturnView()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async void Index_ActionExcecutes_ReturnProductList()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
            var result = (ViewResult) (await _controller.Index());
            var productList = ((List<Product>)result.Model);
            Assert.Equal(3, productList.Count);
        }
        [Fact]
        public async void Details_IdIsNull_ReturnRedirectToActionIndex()
        {
            var result = (RedirectToActionResult)( await _controller.Details(null));
             
            Assert.Equal("Index",result.ActionName);
        }
        [Fact]
        public async void Details_InValidId_ReturnNotFound()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetByIdAsync(0)).ReturnsAsync((Product)null);
            //Act
            var result =(StatusCodeResult)await _controller.Details(0);
            //Assert
            Assert.Equal<int>(404, result.StatusCode);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Details_ValidId_ReturnViewWithProduct(int value)
        {
            //Arrange
            var product = products.FirstOrDefault(y => y.Id == value);
            _mockRepo.Setup(x => x.GetByIdAsync(value)).ReturnsAsync(product);
            //Act
            var result = Assert.IsType<ViewResult>( await _controller.Details(value));
            var resultProduct = Assert.IsAssignableFrom<Product>(result.Model);
            //Assert
            Assert.Equal(product.Id, resultProduct.Id);

        }

        [Fact]
        public void Create_ActionExecutes_ReturnViews()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async void Create_InValidModelState_ReturnProductView()
        {
            _controller.ModelState.AddModelError("Name", "Name field is required.");

            var result =await _controller.Create(products.First());

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Product>(viewResult.Model);
        }

    }
}
