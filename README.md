# OData.QueryBuilder
���������� ����������� ������� OData ������ �� ������ ������ ������

[![Build Status](https://travis-ci.com/ZEXSM/OData.QueryBuilder.svg?branch=master)](https://travis-ci.com/ZEXSM/OData.QueryBuilder)
[![Coverage Status](https://coveralls.io/repos/github/ZEXSM/OData.QueryBuilder/badge.svg?branch=master)](https://coveralls.io/github/ZEXSM/OData.QueryBuilder?branch=master)

## ���������
����� ���������� `OData.QueryBuilder` �� `Visual Studio`, ������� `OData.QueryBuilder` � ���������������� ���������� ���������� ������� `NuGet` ��� ��������� ��������� ������� � ������� ���������� �������:
```
Install-Package OData.QueryBuilder -Version 1.0.0
```

����� �������� ������ �� �������� ������ `dotnet`, ��������� � ��������� ������ ���������:

```
dotnet add -v 1.0.0 OData.QueryBuilder
```

## �������������

1. ������� ��������� �������

    ������� ��� ������ ������� `OData` ������, ���������� ������� ����� ��������� ������� `OData.QueryBuilder` � ��������� ������ ������ � �������� ����:

    ```charp
    var odataQueryBuilder = new ODataQueryBuilder<������ ������ ���������>(<base_url>);
    ```

2. ������� ������ ��� �������� ����� ��������� ������

    ```charp
    odataQueryBuilder.For<������ ��������>(s => s.<������>)
    ```

3. �������� ��� �������

    ������ ��������� ������� ������� �� ����� � ������.
    * ByKey(<����>)
      * Expand
      * Select
      * ToUri 
    * ByList()
      * Expand
      * Filter
      * Select
      * OrderBy
      * OrderByDescending
      * Top
      * Skip
      * Count
      * ToUri 
4. �������� Uri ������� �� �������
    ```charp
    odataQueryBuilder.ToUri()
    ```

## �������

#### ByKey - �� �����
1. ������ �� ����� � ������� `Expand`
```charp
var uri = new ODataQueryBuilder<ODataInfoContainer>("http://mock/odata")
    .For<ODataTypeEntity>(s => s.ODataType)
    .ByKey(223123123)
    .Expand(s => s.ODataKind)
    .ToUri();
```
> http://mock/odata/ODataType(223123123)?$expand=ODataKind

2. ������ �� ����� � ���������� `Expand` � `Select`
```charp
var uri = new ODataQueryBuilder<ODataInfoContainer>("http://mock/odata")
    .For<ODataTypeEntity>(s => s.ODataType)
    .ByKey(223123123)
    .Expand(f =>
    {
        f.For<ODataKindEntity>(s => s.ODataKind)
            .Expand(ff => ff.For<ODataCodeEntity>(s => s.ODataCode)
            .Select(s => s.IdCode));
        f.For<ODataKindEntity>(s => s.ODataKindNew)
            .Select(s => s.IdKind);
        f.For<ODataKindEntity>(s => s.ODataKindNew)
            .Select(s => s.IdKind);
    })
    .Select(s => new { s.IdType, s.Sum })
    .ToUri();
```
> http://mock/odata/ODataType(223123123)?$expand=ODataKind($expand=ODataCode($select=IdCode)),ODataKindNew($select=IdKind),ODataKindNew($select=IdKind)&$select=IdType,Sum

#### ByList - �� ������
1. ������ �� ������ � ������� `Expand`
```charp
var uri = new ODataQueryBuilder<ODataInfoContainer>("http://mock/odata")
    .For<ODataTypeEntity>(s => s.ODataType)
    .ByList()
    .Expand(s => new { s.ODataKind })
    .ToUri();
```
> http://mock/odata/ODataType?$expand=ODataKind
