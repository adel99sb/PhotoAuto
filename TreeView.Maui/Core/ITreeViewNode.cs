﻿namespace TreeView.Maui.Core;

public interface ITreeViewNode
{
    string Name { get; set; }
    string ImagePath { get; set; }
    object Value { get; set; }
    bool IsExtended { get; set; }
}
