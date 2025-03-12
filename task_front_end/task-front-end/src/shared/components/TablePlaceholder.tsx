interface TablePlaceholderProps {
  colSpan?: number;
  rows?: number;
}

export const TablePlaceholder = ({
  colSpan = 5,
  rows = 5,
}: TablePlaceholderProps) => {
  return (
    <>
      {Array.from({ length: rows }).map((_, index) => (
        <tr key={index}>
          {Array.from({ length: colSpan }).map((_, cellIndex) => (
            <td key={cellIndex}>
              <div className="placeholder-glow">
                <span className="placeholder col-12"></span>
              </div>
            </td>
          ))}
        </tr>
      ))}
    </>
  );
};
