class Importers
  class << self
    def RGB
      :RGB
    end

    def CMYK
      :CMYK
    end
  end

  ['rgb', 'cmyk'].each { |f| require File.join 'internal', f }

  def make_importer type
    case type
    when :RGB
      Rgb.new
    when :CMYK
      Cmyk.new
    else
      raise 'unknown importer type'
    end
  end

end
