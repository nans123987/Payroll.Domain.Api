namespace Payroll.Domain.Business.Tests.PayRuleEngineTests
{
    [TestClass]
    public class EmployeeDependentDeductionEngineTests
    {
        private PayrollContext? _context;
        private EmployeeDependentDeductionEngine? _employeeDependentDeductionEngine;
        private ResourceManager? resourceManager;

        [TestInitialize]
        public void Initialize()
        {
            resourceManager = new ResourceManager(typeof(ContextData));
            _employeeDependentDeductionEngine = new EmployeeDependentDeductionEngine();
        }

        [TestMethod]
        public void Process_Employee_Dependent_Deductions_Success()
        {
            //arrange
            var contextString = resourceManager?.GetString("DependentDeductionEngineInputContext");

            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);

            //act
            var processedContext = _employeeDependentDeductionEngine?.Execute(_context);

            //assert
            Assert.AreEqual(processedContext?.EmployeePayPeriodsDeductions.Count, 3);
        }

        [TestMethod]
        public void Process_Employee_WithNoDependents_Dependent_Deductions_Success()
        {
            //arrange
            var contextString = resourceManager?.GetString("DependentDeductionEngineInputContext");

            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);
            _context?.EmployeeDependents.Clear();
           
            //act
            var processedContext = _employeeDependentDeductionEngine?.Execute(_context);


            //assert
            var isAllDependentDeductionZero = processedContext?.EmployeePayPeriodsDeductions.All(x => x.DependentDeductionAmount == 0);
            Assert.IsTrue(isAllDependentDeductionZero);
        }
    }
}
