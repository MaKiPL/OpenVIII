﻿using Microsoft.Xna.Framework;

namespace OpenVIII.Fields.Scripts.Instructions
{
    /// <summary>
    /// Place this entity's model at XCoord, YCoord, ZCoord standing on the given walkmesh triangle.
    /// </summary>
    /// <see cref="http://wiki.ffrtt.ru/index.php?title=FF8/Field/Script/Opcodes/01E_SET3"/>
    public sealed class SET3 : JsmInstruction
    {
        #region Fields

        private readonly Vector3 _pos;
        private readonly int _walkmeshTriangleId;

        #endregion Fields

        #region Constructors

        public SET3(int walkmeshTriangleId, int x, int y, int z) => (_walkmeshTriangleId, _pos.X, _pos.Y, _pos.Z) = (walkmeshTriangleId, x, y, z);

        public SET3(int walkmeshTriangleId, IStack<IJsmExpression> stack)
            : this(walkmeshTriangleId,
                z: ((IConstExpression)stack.Pop()).Int32(),
                y: ((IConstExpression)stack.Pop()).Int32(),
                x: ((IConstExpression)stack.Pop()).Int32())
        {
        }

        #endregion Constructors

        #region Methods

        public override void Format(ScriptWriter sw, IScriptFormatterContext formatterContext, IServices services) => sw.Format(formatterContext, services)
                .Property(nameof(FieldObject.Model))
                .Method(nameof(FieldObjectModel.SetPosition))
                .Argument("walkmeshTriangleId", _walkmeshTriangleId)
                .Argument("x", (int)_pos.X)
                .Argument("y", (int)_pos.Y)
                .Argument("z", (int)_pos.Z)
                .Comment(nameof(SET3));

        public override IAwaitable TestExecute(IServices services)
        {
            var currentObject = ServiceId.Field[services].Engine.CurrentObject;
            currentObject.Model.SetPosition(new WalkMeshCoords(_walkmeshTriangleId, _pos));
            return DummyAwaitable.Instance;
        }

        public override string ToString() => $"{nameof(SET3)}({nameof(_walkmeshTriangleId)}: {_walkmeshTriangleId}, {nameof(_pos)}: {_pos})";

        #endregion Methods
    }
}