class MockFile

  def initialize from_string
    @from_string = from_string
  end

  def read
    @from_string
  end

end
