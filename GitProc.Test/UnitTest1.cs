using GitProc.Services;
using System;
using Xunit;

namespace GitProc.Test
{
    public class UnitTest1
    {

        private readonly ITribunalService _tribunalService;

        public UnitTest1(ITribunalService tribunalService)
        {
            _tribunalService = tribunalService;
        }

        [Fact]
        public void Test1()
        {
            var result = _tribunalService.GetOnlineProcessData(null,new Guid(),null);
            Assert.Null(result);
        }

        [Fact]
        public void Test2()
        {
            var result = _tribunalService.GetOnlineProcessData("0007563-67.2010.8.19.0203", new Guid(), null);
            Assert.NotNull(result);
        }
    }
}
