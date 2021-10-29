function SelectOption({ category }) {
  return(
    <option value={category.categoryId}>{category.name}</option>
  )
}

export default SelectOption