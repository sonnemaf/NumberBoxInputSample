#if WINDOWS_UWP
using Windows.UI.Xaml.Input;
#else
using Microsoft.UI.Xaml.Input;
#endif
using System;
using Windows.System;
using Windows.UI.Core;

namespace Helpers {

    /// <summary>
    /// This helper class is used to avoid letter inputs for a NumberBox. 
    /// Make sure you upgrade your UWP project to at least C# 9.0, we use switch expressions and 
    /// logical pattern matching.
    /// <code>
    /// <NumberBox PreviewKeyDown="{x:Bind helpers:PreviewKeyDownHandler.AcceptNumbersOnly}" />
    /// </code>
    /// </summary>
    static class PreviewKeyDownHandler {

#if WINDOWS_UWP
        private static Func<VirtualKey, CoreVirtualKeyStates> _keyStateFunction = CoreWindow.GetForCurrentThread().GetKeyState;
#else
        private static Func<VirtualKey, CoreVirtualKeyStates> _keyStateFunction = Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread;
#endif

        private static bool IsModifierKeyDown() {
            return _keyStateFunction(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down) ||
                   _keyStateFunction(VirtualKey.LeftMenu).HasFlag(CoreVirtualKeyStates.Down) ||
                   _keyStateFunction(VirtualKey.RightMenu).HasFlag(CoreVirtualKeyStates.Down) ||
                   _keyStateFunction(VirtualKey.LeftWindows).HasFlag(CoreVirtualKeyStates.Down) ||
                   _keyStateFunction(VirtualKey.RightWindows).HasFlag(CoreVirtualKeyStates.Down);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Sender is used")]
        public static void RejectLetters(object sender, KeyRoutedEventArgs e) {
            if (IsModifierKeyDown()) {
                return;
            }
            if (e.Key is >= VirtualKey.A and <= VirtualKey.Z) {
                e.Handled = true;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Sender is used")]
        public static void AcceptNumbersOnly(object sender, KeyRoutedEventArgs e) {
            if (IsModifierKeyDown()) {
                return;
            }

            e.Handled = e.Key switch {
                >= VirtualKey.Number0 and <= VirtualKey.Number9 => false,
                >= VirtualKey.NumberPad0 and <= VirtualKey.NumberPad9 => false,
                VirtualKey.Left => false,
                VirtualKey.Right => false,
                VirtualKey.Up => false,
                VirtualKey.Down => false,
                VirtualKey.PageUp => false,
                VirtualKey.PageDown => false,
                VirtualKey.Tab => false,
                VirtualKey.Home => false,
                VirtualKey.End => false,
                VirtualKey.Back => false,
                VirtualKey.Delete => false,
                VirtualKey.Enter => false,
                VirtualKey.Decimal => false,
                (VirtualKey)188 => false, // Comma
                (VirtualKey)189 => false, // Minus
                (VirtualKey)190 => false, // Dot
                _ => true,
            };
        }
    }
}
