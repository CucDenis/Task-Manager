import { useMemo } from "react";

export const useUnique = <T>(
  items: T[],
  getter: (item: T) => string
): string[] => {
  return useMemo(() => {
    return [...new Set(items.map(getter))];
  }, [items, getter]);
};
