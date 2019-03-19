import PageResult from "../DTOs/PageResult";
import Product from "../DTOs/Product";


export default class ProductService {

    getProducts(categoryID:number = 1, pageIndex:number = 1,pageSize:number = 2) : Promise<PageResult<Product>> {
        let query = "?categoryID=" + categoryID;
        query += "&pageIndex=" + pageIndex;
        query += "&pageSize="  + pageSize;      
        return fetch('/api/products' + query).then(res => res.json());
    }
}