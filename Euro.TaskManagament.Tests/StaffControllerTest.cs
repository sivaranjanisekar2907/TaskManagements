using Microsoft.AspNetCore.Mvc;
using Moq;
using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Controllers;
using Euro.TaskManagement.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Euro.TaskManagement.Tests
{
    public class StaffControllerTest
    {
        private readonly StaffController controller;
        private readonly Mock<IStaffService> mockServ;
        public StaffControllerTest()
        {
            mockServ = new Mock<IStaffService>();
            controller = new StaffController();
           
        }

        [Theory]
        [InlineData(1)]
        public async Task ToValidate_StaffDetails_OutPutTestAsync(int staffId)
        {
            // Arrange
            mockServ.Setup(repo => repo.GetStaffDetailAsync(staffId)).ReturnsAsync(MockObjects.GetStaffData());

            // Act 
            var result = await controller.GetStaffDetail(staffId);

            // Assert 
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var actualTerms = Assert.IsType<List<Staff>>(actionResult.Value);
            Assert.IsType<List<Staff>>(actualTerms);
        }


        /// <summary>
        /// Test case to check whether controller returns the data
        /// </summary>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async Task ToValidate_DeleteStaff_OutputTestAsync(int staffId)
        {
            // Arrange
            var delete = MockObjects.DeleteStaff(staffId);
            mockServ.Setup(repo => repo.DeleteStaffAsync(delete));

            // Act 
            var result = await controller.DeleteStaff(delete);

            // Assert 
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var actualTerms = Assert.IsType<List<Staff>>(actionResult.Value);
            Assert.IsType<List<Staff>>(actualTerms);

        }
    }
}
