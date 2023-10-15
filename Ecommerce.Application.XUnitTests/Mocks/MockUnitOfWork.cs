using Ecommerce.Application.Persistence.Abstractions;

namespace Ecommerce.Application.XUnitTests.Mocks;

public static class MockUnitOfWork
{

    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        #region object moqUOW
        //object moqmoqUOW
        var moqUOW = new Mock<IUnitOfWork>();
        #endregion

        //SetUp();

        #region Commit
        //Commit
        moqUOW.Setup(x => x.Commit());
        #endregion


        return moqUOW;




    }

}
