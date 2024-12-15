const CardInShppingCart = () => {
    return (
        
    <div className="flex border rounded-lg shadow-sm p-4">
      <div className="w-16 h-16 bg-gray-300 rounded-md mr-4"></div>
      <div className="flex-1">
        <h3 className="font-semibold text-lg">Име на събитието</h3>
        <p className="text-yellow-500 text-sm flex items-center mb-2">
          ⭐ 5.0 (34)
        </p>
        <div className="text-gray-500 text-sm space-y-1">
          <p>🗓 ден, дата, час</p>
          <p>👤 3 възрастни</p>
          <p>💲 $$$</p>
        </div>
      </div>
      <button className="bg-teal-500 text-white h-1/3 px-4 py-2 rounded-2xl shadow hover:bg-teal-400">
        Изтрий
      </button>
    </div>
  )
}
export default CardInShppingCart;