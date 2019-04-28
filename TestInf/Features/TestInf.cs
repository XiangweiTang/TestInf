using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf.Features
{
    public abstract class TestInf
    {
        public TestInf() { }
        public DummyConfig Cfg = new DummyConfig();
        public void LoadAndRun(TestInfArgs arg)
        {
            Cfg.Load(arg);
        }        
    }
}
