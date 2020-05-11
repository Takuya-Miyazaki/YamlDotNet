﻿using YamlDotNet.Core;
using YamlDotNet.Helpers;

namespace YamlDotNet.Representation.Schemas
{
    /// <summary>
    /// Implements the YAML 1.1 schema: <see href="https://yaml.org/spec/1.2/spec.html#id2802346"/>.
    /// </summary>
    public sealed class Yaml11Schema : RegexBasedSchema
    {
        private Yaml11Schema() : base(BuildMappingTable(), FailsafeSchema.String) { }

        public static readonly Yaml11Schema Instance = new Yaml11Schema();

        private static RegexTagMappingTable BuildMappingTable() => new RegexTagMappingTable
        {
            {
                "^(y|Y|yes|Yes|YES|n|N|no|No|NO|true|True|TRUE|false|False|FALSE|on|On|ON|off|Off|OFF)$",
                YamlTagRepository.Boolean,
                s =>
                {
                    // Assumes that the value matches the regex
                    return s.Value[0] switch
                    {
                        'y' => true, // y, yes
                        'Y' => true, // Y, Yes, YES
                        't' => true, // true
                        'T' => true, // True, TRUE
                        'n' => false, // n, no
                        'N' => false, // N, No, NO
                        _ => s.Value[1] switch
                        {
                            'n' => true, // on, On
                            'N' => true, // ON
                            _ => false
                        }
                    };
                },
                native => true.Equals(native) ? "true" : "false"
            },
            {
                "^[-+]?0b[0-1_]+$", // Base 2
                YamlTagRepository.Integer,
                s => IntegerParser.ParseBase2(s.Value),
                JsonSchema.FormatInteger
            },
            {
                "^[-+]?0[0-7_]+$", // Base 8
                YamlTagRepository.Integer,
                s => IntegerParser.ParseBase8(s.Value),
                JsonSchema.FormatInteger
            },
            {
                "^[-+]?(0|[1-9][0-9_]*)$", // Base 10
                YamlTagRepository.Integer,
                s => IntegerParser.ParseBase10(s.Value),
                JsonSchema.FormatInteger
            },
            {
                "^[-+]?0x[0-9a-fA-F_]+$", // Base 16
                YamlTagRepository.Integer,
                s => IntegerParser.ParseBase16(s.Value),
                JsonSchema.FormatInteger
            },
            {
                "^[-+]?[1-9][0-9_]*(:[0-5]?[0-9])+$", // Base 60
                YamlTagRepository.Integer,
                s => IntegerParser.ParseBase60(s.Value),
                JsonSchema.FormatInteger
            },
            {
                @"^[-+]?([0-9][0-9]*)?\.[0-9]*([eE][-+][0-9]+)?$", // Base 10 unseparated
                YamlTagRepository.FloatingPoint,
                s => FloatingPointParser.ParseBase10Unseparated(s.Value),
                JsonSchema.FormatFloatingPoint
            },
            {
                @"^[-+]?([0-9][0-9_]*)?\.[0-9_]*([eE][-+][0-9]+)?$", // Base 10 separated
                YamlTagRepository.FloatingPoint,
                s => FloatingPointParser.ParseBase10Separated(s.Value),
                JsonSchema.FormatFloatingPoint
            },
            {
                @"^[-+]?[0-9][0-9_]*(:[0-5]?[0-9])+\.[0-9_]*$", // Base 60
                YamlTagRepository.FloatingPoint,
                s => FloatingPointParser.ParseBase60(s.Value),
                JsonSchema.FormatFloatingPoint
            },
            {
                @"^[-+]?\.(inf|Inf|INF)$", // Infinity
                YamlTagRepository.FloatingPoint,
                s => s.Value[0] switch
                {
                    '-' => double.NegativeInfinity,
                    _ => double.PositiveInfinity
                },
                JsonSchema.FormatFloatingPoint
            },
            {
                @"^\.(nan|NaN|NAN)$", // Non a number
                YamlTagRepository.FloatingPoint,
                s => double.NaN,
                JsonSchema.FormatFloatingPoint
            },
            {
                "^(null|Null|NULL|~?)$", // Null
                YamlTagRepository.Null,
                s => null,
                _ => "null"
            },
        };
    }
}
