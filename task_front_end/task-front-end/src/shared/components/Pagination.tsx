import { useMemo } from "react";

interface PaginationProps {
  currentPage: number;
  pageCount: number;
  onPageChange: (page: number) => void;
}

export const Pagination = ({
  currentPage,
  pageCount,
  onPageChange,
}: PaginationProps) => {
  const pages = useMemo(() => {
    const items: (number | string)[] = [];

    if (pageCount <= 5) {
      return Array.from({ length: pageCount }, (_, i) => i + 1);
    }

    // Always show first page
    items.push(1);

    // Calculate range
    const start = Math.max(2, currentPage - 1);
    const end = Math.min(pageCount - 1, currentPage + 1);

    // Add ellipsis before range if needed
    if (start > 2) {
      items.push("...");
    }

    // Add range around current page
    for (let i = start; i <= end; i++) {
      items.push(i);
    }

    // Add ellipsis after range if needed
    if (end < pageCount - 1) {
      items.push("...");
    }

    // Always show last page
    items.push(pageCount);

    return items;
  }, [currentPage, pageCount]);

  return (
    <nav aria-label="Page navigation">
      <ul className="pagination pagination-sm">
        <li className={`page-item ${currentPage === 1 ? "disabled" : ""}`}>
          <button
            className="page-link"
            onClick={() => onPageChange(1)}
            disabled={currentPage === 1}
          >
            «
          </button>
        </li>
        <li className={`page-item ${currentPage === 1 ? "disabled" : ""}`}>
          <button
            className="page-link"
            onClick={() => onPageChange(currentPage - 1)}
            disabled={currentPage === 1}
          >
            ‹
          </button>
        </li>

        {pages.map((page, index) => (
          <li
            key={index}
            className={`page-item ${page === currentPage ? "active" : ""} ${
              typeof page === "string" ? "disabled" : ""
            }`}
          >
            <button
              className="page-link"
              onClick={() => typeof page === "number" && onPageChange(page)}
              disabled={typeof page === "string"}
            >
              {page}
            </button>
          </li>
        ))}

        <li
          className={`page-item ${currentPage === pageCount ? "disabled" : ""}`}
        >
          <button
            className="page-link"
            onClick={() => onPageChange(currentPage + 1)}
            disabled={currentPage === pageCount}
          >
            ›
          </button>
        </li>
        <li
          className={`page-item ${currentPage === pageCount ? "disabled" : ""}`}
        >
          <button
            className="page-link"
            onClick={() => onPageChange(pageCount)}
            disabled={currentPage === pageCount}
          >
            »
          </button>
        </li>
      </ul>
    </nav>
  );
};
