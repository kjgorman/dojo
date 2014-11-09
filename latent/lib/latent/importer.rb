class Importer

  require File.join 'orders', 'recorder'

  def initialize converter
    @converter = converter
  end

  def import file_stream
    contents = file_stream.read

    result = @converter.convert contents

    Recorder.new.record_order result

    result
  end

end
