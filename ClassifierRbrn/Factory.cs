﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifier.Nodes;

namespace Classifier
{
    public interface IFactory
    {
        void Execute();
        IOutputData outputData { get; }
    }

    public class Factory : IFactory
    {
        INodesCollection mf = new NodesCollection();
        IInputData data;
        ICodes Codes { get; set; }
        ICodeSeeker SearchingResult { get; set; }
        ICodeHandler processing { get; set; }
        ITypeAndKind Types { get; set; }
        IBTI Bti { get; set; }
        public IOutputData outputData { get; private set; }

        public Factory(IInputData data)
        {
            this.data = data;
            Codes = new Codes(mf);
        }

        private IBTI CreateBTI()
        {
            return new BTI(data.BtiVri, data.Lo_lvl, data.Mid_lvl, data.Hi_lvl);
        }

        private ICodeSeeker CreateISearch()
        {
            return new CodeSeeker(data.Vri_doc, Codes, mf);
        }

        private ICodeHandler CreateProcessing()
        {
            return new CodeHandler(Codes, Bti, data.Vri_doc, data.Area, mf);
        }

        private ITypeAndKind CreateTypes()
        {
            return new TypeAndKind(Codes);
        }

        private IOutputData CreateOutputData()
        {
            return new OutputData(Codes.Show, SearchingResult.Matches,
                SearchingResult.IsMainSearch, SearchingResult.IsPZZSearch,
                    SearchingResult.IsFederalSearch, processing.Landscaping,
                        processing.Maintenance, Types.Type, Types.Kind);
        }

        public void Execute()
        {
            SearchingResult = CreateISearch();
            Types = CreateTypes();
            Bti = CreateBTI();
            processing = CreateProcessing();
           
            processing.Cutter += Types.CutterDelegate;
            SearchingResult.SendFederalCode += processing.IsFederal;

            SearchingResult.Seek();
            processing.FullProcessing();

            if (IsNoResult())
            {
                IInputData newData = new InputData(data.VRI_KLASSI, data.Area
                    , data.BtiVri, data.Lo_lvl, data.Mid_lvl, data.Hi_lvl);

                IFactory newFactory = new Factory(newData);
                newFactory.Execute();
                outputData = newFactory.outputData;
            }
            else
                outputData = CreateOutputData();
        }

        private bool IsNoResult()
        {
            return !string.IsNullOrEmpty(data.VRI_KLASSI) && (Types.Type == 999 || Types.Type == 777 || string.IsNullOrEmpty(data.Vri_doc) || Codes.Show == "12.3");
        }
    }
}
