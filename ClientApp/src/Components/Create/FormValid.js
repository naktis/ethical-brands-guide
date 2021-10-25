function FormValid(brand) {
  if (brand.name === null || brand.description === null || brand.companyId === null) 
    return false;

  if(brand.name.length >= 2 &&
    brand.name.length <= 40 &&
    brand.description.length >= 5 &&
    brand.description.length <= 200 &&
    brand.companyId !== 0)
    return true;

  return false;
}

export default FormValid;