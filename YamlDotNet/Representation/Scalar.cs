﻿//  This file is part of YamlDotNet - A .NET library for YAML.
//  Copyright (c) Antoine Aubry and contributors

//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//  of the Software, and to permit persons to whom the Software is furnished to do
//  so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.

using System;
using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Core;
using HashCode = YamlDotNet.Core.HashCode;

namespace YamlDotNet.Representation
{
    public sealed class Scalar : Node, INodePathSegment
    {
        public string Value { get; }

        public Scalar(INodeMapper mapper, string value)
            : this(mapper, value, Mark.Empty, Mark.Empty)
        {
        }

        public Scalar(INodeMapper mapper, string value, Mark start, Mark end) : base(mapper, start, end)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override NodeKind Kind => NodeKind.Scalar;

        string INodePathSegment.Value => Value;

        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);

        public override bool Equals([AllowNull] Node other)
        {
            return base.Equals(other)
                && this.Value.Equals(((Scalar)other).Value); // 'other' is always a Scalar when base.Equals returns true
        }

        public override int GetHashCode()
        {
            return HashCode.CombineHashCodes(base.GetHashCode(), Value);
        }

        public override string ToString() => $"Scalar {Tag} '{Value}'";
    }
}