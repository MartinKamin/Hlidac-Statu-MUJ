﻿// ---------------------------------------------------------------------------
// <copyright file="VerticalTextLayout.cs" owner="svm-git">
//
//  Copyright (c) 2018 svm-git
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
//
// </copyright>
// ---------------------------------------------------------------------------

namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Vertical layout for cue text.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-vertical-text-cue-setting for details.</remarks>
    public enum VerticalTextLayout
    {
        /// <summary>
        /// Right to left layout.
        /// </summary>
        RightToLeft,

        /// <summary>
        /// Left to right layout.
        /// </summary>
        LeftToRight
    }
}
