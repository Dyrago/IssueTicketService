export type TicketsSearchFilters = {
    Contains: string | null;
    DateFrom: Date | null;
    DateTo: Date | null;
    ResolverId: string | null;
    ReporterId: string | null;
    TicketStatusId: number | null;
}