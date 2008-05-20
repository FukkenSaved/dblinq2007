﻿#region MIT license
// 
// Copyright (c) 2007-2008 Jiri Moudry
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
#endregion

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using DbLinq.Linq.Data.Sugar.Pieces;

namespace DbLinq.Linq.Data.Sugar.Pieces
{
    /// <summary>
    /// This is the common class for all operations pieces
    /// </summary>
    [DebuggerDisplay("OperationPiece {Operation}")]
    public class OperationPiece : Piece
    {
        public OperationType Operation { get; private set; }
        public Expression OriginalExpression { get; set; }

        public OperationPiece(OperationType operation, params Piece[] operands)
            : base(operands)
        {
            Operation = operation;
        }

        public OperationPiece(OperationType operation, IList<Piece> operands)
            : base(operands)
        {
            Operation = operation;
        }

        #region  ctors provided to ease OperationPiece creation

        public OperationPiece(ExpressionType operation, params Piece[] operands)
            : this((OperationType)operation, operands)
        {
        }

        public OperationPiece(ExpressionType operation, IList<Piece> operands)
            : this((OperationType)operation, operands)
        {
        }

        #endregion
    }
}
