using System;
using Xunit;
using MS_Authentication;

namespace test_project
{
    public class UnitTest1
    {
       
        [Fact]
        public void Test1()
        {
            string expected = "KapilPatel";
            string actual =  Dummy.getFullName("Kapil","Patel");

            Assert.Equal(expected,actual);
        }

    }
}
