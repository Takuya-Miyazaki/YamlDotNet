//  This file is part of YamlDotNet - A .NET library for YAML.
//  Copyright (c) 2008, 2009, 2010, 2011 Antoine Aubry
    
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
using System.Text;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace Samples
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup the input
			Stream input = new MemoryStream(Encoding.UTF8.GetBytes(Document));

			// Load the stream
			YamlStream yaml = new YamlStream();
			yaml.Load(input);

			// Examine the stream
			YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
			foreach(KeyValuePair<YamlNode, YamlNode> entry in mapping.Children)
			{
				Console.WriteLine(((YamlScalarNode)entry.Key).Value);
			}
		}

		private const string Document = @"---
		                                receipt:    Oz-Ware Purchase Invoice
		                                date:        2007-08-06
		                                customer:
		                                given:   Dorothy
		                                family:  Gale

		                                items:
		                                - part_no:   A4786
		                                descrip:   Water Bucket (Filled)
		                                price:     1.47
		                                quantity:  4

		                                - part_no:   E1628
		                                descrip:   High Heeled ""Ruby"" Slippers
		                                price:     100.27
		                                quantity:  1

		                                bill-to:  &id001
		                                street: |
		                                123 Tornado Alley
		                                Suite 16
		                                city:   East Westville
		                                state:  KS

		                                ship-to:  *id001

		                                specialDelivery:  >
		                                Follow the Yellow Brick
		                                Road to the Emerald City.
		                                Pay no attention to the
		                                man behind the curtain.
		                                ...";
	}
}
