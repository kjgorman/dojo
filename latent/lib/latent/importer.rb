class Importer

  require File.join 'orders', 'recorder'

  def initialize converter
    @converter = converter
  end

  def import file_stream
    contents = file_stream.read

    lines = contents.split '\n'

    result = lines.map { |l| @converter.convert l }

    Recorder.new.record_order result

    if result.length > 1
      result
    else
      result.at 0
    end
  end

end
