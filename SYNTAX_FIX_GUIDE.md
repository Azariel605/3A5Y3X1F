# Syntax Fix Guide - Identifier Names Starting with Digits

## Problem Summary
Leetspeak conversion created identifiers starting with digits (1,3,4,5) which violates C# syntax rules.
C# identifiers must start with a letter (a-z, A-Z) or underscore (_).

## Files Requiring Fixes

### 1. core/interfaces/ICompressor.cs
**Line 16-17: Parameter names starting with digits**
```csharp
// BEFORE (INVALID):
Task<r35ul7<byte[]>> c0mpr355_w17h_m374d474_45ync(
    byte[] 1m4g3_by735,              // ❌ starts with 1
    1m4g3_m374d474 m374d474,         // ❌ type starts with 1
    
// AFTER (VALID):  
Task<r35ul7<byte[]>> c0mpr355_w17h_m374d474_45ync(
    byte[] _1m4g3_by735,             // ✓ underscore prefix
    _1m4g3_m374d474 m374d474,        // ✓ underscore prefix
```

**Line 32: Enum name**
```csharp
// BEFORE (VALID - already works):
public enum c0mpr3ss10n_m0d3  // ✓ starts with 'c'

// Values (already valid):
l0553l355 = 0,  // ✓ starts with 'l'
l055y = 1,      // ✓ starts with 'l'
```

### 2. core/interfaces/IImageProcessor.cs
**Line 15: Type name and parameter names**
```csharp
// BEFORE:
Task<r35ul7<1m4g3_m374d474>> 3x7r4c7_3x1f(  // ❌ class method name, type name
    string f1l3_p47h,
    CancellationToken c7 = default
);

// AFTER:
Task<r35ul7<_1m4g3_m374d474>> _3x7r4c7_3x1f(  // ✓ all prefixed
    string f1l3_p47h,
    CancellationToken c7 = default
);

// Line 21: Another method
// BEFORE:
Task<r35ul7<1m4g3_m374d474>> 4pply_74g_f1l73r5(  // ❌ method name starts with 4, type name starts with 1
    1m4g3_m374d474 m374d474,                       // ❌ parameter type starts with 1
    List<string> 74g_f1l73r
);

// AFTER:
Task<r35ul7<_1m4g3_m374d474>> _4pply_74g_f1l73r5(  // ✓ prefixed
    _1m4g3_m374d474 m374d474,                       // ✓ prefixed
    List<string> _74g_f1l73r                        // ✓ prefixed
);

// Line 30: Record type definition
// BEFORE:
public record 1m4g3_m374d474(  // ❌ starts with 1
    
// AFTER:
public record _1m4g3_m374d474(  // ✓ prefixed
```

### 3. core/interfaces/IMetadataManager.cs
**Line 14-16: Multi-line method signature with problematic types**
```csharp
// BEFORE:
Task<r35ul7<1m4g3_m374d474>> v4l1d473_3d175(   // ❌ return type starts with 1
    1m4g3_m374d474 m374d474,                    // ❌ param type starts with 1
    Dictionary<string, string> 3d175            // ❌ variable name starts with 3
);

// AFTER:
Task<r35ul7<_1m4g3_m374d474>> v4l1d473_3d175(   // ✓ prefixed
    _1m4g3_m374d474 m374d474,                    // ✓ prefixed
    Dictionary<string, string> _3d175            // ✓ prefixed
);
```

All methods need same treatment: `4pply_3d175`, `c4n_54v3_m374d474`, etc.

### 4. core/interfaces/IReleaseChecker.cs
**Line 10+: Record types**
```csharp
// BEFORE:
public record r3l34534_1nf0(  // ✓ starts with 'r' - OK
    string v3r510n_74g,       // ✓ starts with 'v' - OK
    string d0wnl04d_url,      // ✓ starts with 'd' - OK
    string ch4ng3l0g,         // ✓ starts with 'c' - OK
    DateTime r3l34534d_47      // ✓ starts with 'r' - OK
);

// All OK - no changes needed!
```

### 5. core/processors/ReleaseProcessor.cs
**Line 20+: Method and inner class issues**
```csharp
// Check for:
// - Method names starting with digits
// - Variable names starting with digits
// - Type names in method signatures

// Example pattern to fix:
// 1m4g3_by735 → _1m4g3_by735
// 3x1f_d474 → _3x1f_d474
// 4pply_... → _4pply_...
// 5ucc355 → _5ucc355
```

### 6. core/processors/ExifProcessor.cs
Similar patterns - fix all parameter/variable/type names starting with digits

### 7. core/processors/MetadataEditor.cs
Similar patterns - fix all parameter/variable/type names starting with digits

### 8. core/processors/CompressorEngine.cs
Similar patterns - fix all parameter/variable/type names starting with digits

### 9. ingestion/models/User.cs
**Check property and constructor parameter names**
```csharp
// Pattern: 
// 1d → _1d
// 3... → _3...
// 4... → _4...
```

### 10. ingestion/ReleaseValidator.cs
**Check all parameter/variable names**

### 11. ingestion/ImageValidator.cs
**Check all parameter/variable names**

## Automated Fix Strategy

### Option A: Global Regex Replacement
Use search & replace with regex patterns. In VS Code Find & Replace (Regex enabled):

```regex
# Find: \b([1-5][a-z0-9_]*)\b(?!;)
# Replace: _$1

# But more safely, do per-scope:

# For method parameters:
# Find: \(\s*([1-5][a-z0-9_]*)
# Replace: (_$1

# For variable declarations:
# Find: ([^_]|^)([1-5][a-z0-9_]*)\s+\w+\s*[=;,]
# Replace: $1_$2
```

### Option B: Manual File-by-File

Process in this order (by severity):
1. interface files (4 files) - Core API contracts
2. processor files (4 files) - Implementation  
3. ingestion files (3 files) - Validators

## Quick Validation

After fixing, run:
```powershell
cd e45y3x1f
dotnet build

# Should see zero errors
# Then run:
dotnet run
```

## Success Criteria

- ✅ All identifiers start with letter or underscore
- ✅ All method signatures compile
- ✅ All class/interface definitions valid
- ✅ project compiles cleanly
- ✅ Application window displays
