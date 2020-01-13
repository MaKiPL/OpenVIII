﻿using System;


namespace OpenVIII.Fields.Scripts.Instructions
{
    internal sealed class GETTIMER : JsmInstruction
    {
        public GETTIMER()
        {
        }

        public GETTIMER(Int32 parameter, IStack<IJsmExpression> stack)
            : this()
        {
        }

        public override String ToString()
        {
            return $"{nameof(GETTIMER)}()";
        }
    }
}