﻿using System;


namespace OpenVIII.Fields.Scripts.Instructions
{
    internal sealed class INITSOUND : JsmInstruction
    {
        public INITSOUND()
        {
        }

        public INITSOUND(Int32 parameter, IStack<IJsmExpression> stack)
            : this()
        {
        }

        public override String ToString()
        {
            return $"{nameof(INITSOUND)}()";
        }
    }
}