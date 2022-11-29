using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Payroll.Domain.Business.PayRuleEngines;
using System;
using System.Linq;
using System.Resources;

namespace Payroll.Domain.Business.Tests.PayRuleEngineTests
{
    [TestClass]
    public class EmployeeDeductionEngineTest
    {
        private PayrollContext? _context;
        private EmployeeDeductionEngine? _employeeDeductionEngine;
        private ResourceManager? resourceManager;

        [TestInitialize]
        public void Initialize() 
        {
            resourceManager = new ResourceManager(typeof(ContextData));
            _employeeDeductionEngine = new EmployeeDeductionEngine();
        }

        [TestMethod]
        public void Process_Employee_Deductions_Success() 
        {
            //arrange
            var contextString = resourceManager?.GetString("DependentDeductionEngineInputContext");
            ArgumentNullException.ThrowIfNull(contextString);
            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);
            //act
            var processedContext = _employeeDeductionEngine?.Execute(_context);

            //assert
            Assert.AreEqual(processedContext?.EmployeePayPeriodsDeductions.Count, 3);
        }

        [TestMethod]
        public void Process_EmployeeDeductions_With_No_Enrollments() 
        {
            //arrange
            var contextString = resourceManager?.GetString("DependentDeductionEngineInputContext");
            ArgumentNullException.ThrowIfNull(contextString);
            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);
            _context?.EmployeeBenefits.Clear();

            //act
            var processedContext = _employeeDeductionEngine?.Execute(_context);

            //assert
            Assert.AreEqual(processedContext?.EmployeePayPeriodsDeductions.Count, 0);
        }

        [TestMethod]
        public void Process_EmployeeDeductions_With_No_ActiveBenefitPlans()
        {
            //arrange
            var contextString = resourceManager?.GetString("DependentDeductionEngineInputContext");
            ArgumentNullException.ThrowIfNull(contextString);
            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);
            _context?.BenefitPlans.Clear();

            //act
            var processedContext = _employeeDeductionEngine?.Execute(_context);

            //assert
            Assert.AreEqual(processedContext.EmployeePayPeriodsDeductions.Count, 0);
        }

        [TestMethod]
        public void Process_EmployeeDeductions_With_Discount() { 
            //arrange
            var contextString = resourceManager?.GetString("EmployeeDeductionIngineInputWithDiscount");
            _context = JsonConvert.DeserializeObject<PayrollContext>(contextString);

            //act
            var processedContext = _employeeDeductionEngine?.Execute(_context);

            //Assert
            Assert.AreEqual(processedContext?.EmployeePayPeriodsDeductions.Count, 1);
            Assert.AreEqual(processedContext?.EmployeePayPeriodsDeductions.First().EmployeeDeductionAmount, (decimal)34.62);
        }

    }
}
