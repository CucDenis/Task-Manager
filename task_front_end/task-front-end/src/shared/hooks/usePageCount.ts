import { useMemo } from "react";

export const usePageCount = <T>(
  data: T[],
  currentPage: number,
  itemsPerPage: number = 10
) => {
  return useMemo(() => {
    const pageCount = Math.ceil(data.length / itemsPerPage);
    const paginatedData = data.slice(
      (currentPage - 1) * itemsPerPage,
      currentPage * itemsPerPage
    );

    return {
      pageCount,
      paginatedData,
    };
  }, [data, currentPage, itemsPerPage]);
};
