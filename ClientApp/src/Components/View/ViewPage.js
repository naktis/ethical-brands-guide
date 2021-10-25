import React from "react";
import { useParams } from 'react-router-dom'
import GenericPage from "../Shared/GenericPage";
import BrandDetails from "./BrandDetails";
import './View.css';

function ViewPage() {
  let { id } = useParams();

  return (
		<GenericPage>
			<BrandDetails id={id}/>
		</GenericPage>
	);
}

export default ViewPage;