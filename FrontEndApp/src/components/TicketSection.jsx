const TicketSection = () => {
  return (
    <div className="lg:col-span-2">
      <div className="bg-white rounded-lg shadow-lg p-6 space-y-5">
        <div className="flex justify-between border-b pb-2">
          <div>
            <p className="font-semibold">Име на събитие 1</p>
            <p className="text-sm text-gray-500">Дата</p>
          </div>
          <p className="text-lg font-bold">$$$</p>
        </div>
        <div className="flex justify-between">
          <div>
            <p className="font-semibold">Име на събитие 2</p>
            <p className="text-sm text-gray-500">Дата</p>
          </div>
          <p className="text-lg font-bold">$$$</p>
        </div>
      </div>
    </div>
  );
}
export default TicketSection;
