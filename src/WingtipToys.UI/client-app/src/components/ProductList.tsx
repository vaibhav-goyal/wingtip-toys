import * as React from 'react';
import Product from '../DTOs/Product';
import { func } from 'prop-types';


export interface Props {
    items? : Product[]
}

export default class ProductList extends React.Component<Props,object> {  

    render() {
        const { items = new Array<Product>() } = this.props;        
        const rows = items.map(renderProductRow);
        return (            
            <table>
                {rows}            
            </table>
        );       
    }
}

function renderProductRow(product:Product) : JSX.Element {
    return (
        <tr key={product.productId}>
            <td>
                <img height="75" src={"assets/" + product.imagePath} width="100"></img>
            </td>
            <td>{product.productName}
                <br/>
                <span className="ProductPrice"><strong>Price:</strong>{product.unitPrice}</span>
            </td>
        </tr>
    );
}