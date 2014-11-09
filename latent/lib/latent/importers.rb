class Importers
  class << self
    def RGB
      :RGB
    end

    def CMYK
      :CMYK
    end
  end

  require 'importer'
  ['rgb', 'cmyk'].each { |f| require File.join 'internal', f }

  def make_importer type
    case type
    when :RGB
      Importer.new Rgb.new
    when :CMYK
      Importer.new Cmyk.new
    else
      raise 'unknown importer type'
    end
  end

end
