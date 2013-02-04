﻿using System;
using System.Collections.Generic;

namespace Cxxi
{
    public class Parser
    {
        public Library Library { get; private set; }
        private readonly DriverOptions options;

        public Parser(DriverOptions options)
        {
            this.options = options;
            Library = new Library(options.OutputNamespace, options.LibraryName);
        }

        public void ParseHeaders(IEnumerable<string> headers)
        {
            foreach (var header in headers)
                ParseHeader(header);
        }

        public ParserResult ParseHeader(string file)
        {
            var parserOptions = new ParserOptions
            {
                Library = Library,
                FileName = file,
                Verbose = false,
                IncludeDirs = options.IncludeDirs,
                Defines = options.Defines,
                toolSetToUse = options.ToolsetToUse
            };

            var result = ClangParser.Parse(parserOptions);
            HeaderParsed(file, result);

            return result;
        }

        public Action<string, ParserResult> HeaderParsed = delegate {};
    }
}