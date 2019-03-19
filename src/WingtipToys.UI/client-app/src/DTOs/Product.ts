export default interface Product {
    productId: number;
    productName: string;
    description: string;
    imagePath: string;
    unitPrice: number;
    categoryId: number;
    category?: any;
}