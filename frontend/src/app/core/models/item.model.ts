export type ItemCondition = 'New' | 'LikeNew' | 'Good' | 'Used';

export interface Item {
    id: number;
    title: string;
    condition: ItemCondition;
    categoryId: number;
    categoryName: string;
    ownerId: string;
    ownerFullName: string;
    imageUrl: string | null;
    isActive: boolean;
}
export const CONDITION_LABELS: Record<ItemCondition, string> = {
    New: 'Nuevo',
    LikeNew: 'Como nuevo',
    Good: 'Bueno',
    Used: 'Usado',
};