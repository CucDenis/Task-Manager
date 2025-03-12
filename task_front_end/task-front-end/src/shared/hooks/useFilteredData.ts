import { useMemo } from "react";

type FilterFunction<T> = (item: T) => boolean;

export const useFilteredData = <T extends Record<string, unknown>>(
  data: T[],
  search: string,
  filterFunctions: FilterFunction<T>[]
) => {
  return useMemo(() => {
    return data.filter((item) => {
      const matchesSearch = Object.values(item).some((val) =>
        val?.toString().toLowerCase().includes(search.toLowerCase())
      );

      const matchesFilters = filterFunctions.every((filterFn) =>
        filterFn(item)
      );

      return matchesSearch && matchesFilters;
    });
  }, [data, search, filterFunctions]);
};
