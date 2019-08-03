export interface TableListItem {
  id: string,
  name: string,
  actionCode: string,
  menuId: string,
  status: number;
  menuName: string;
}
export interface TableListPagination {
  total: number;
  pageSize: number;
  current: number;
}

export interface TableListDate {
  data: TableListItem[];
  pagination: Partial<TableListPagination>;
}

export interface TableListParams {
  sorter: string;
  status: string;
  name: string;
  pageSize: number;
  currentPage: number;
}

